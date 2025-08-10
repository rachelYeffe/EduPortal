import { Component, ComponentFactoryResolver, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { ChildDetails } from '../../Models/ChildDetails';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ChildrenExcelUploadService } from '../../Services/children-excel-upload.service';
import { SearchResult } from '../../Models/SearchResult';
import { NgFor, NgIf } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import Swal from 'sweetalert2';
import { GraduateComponent } from '../graduate/graduate.component';
import { Graduate } from '../../Models/Graduate';
import { YeshivaStudent } from '../../Models/YeshivaStudent';
import { YeshivaStudentComponent } from '../yeshiva-student/yeshiva-student.component';
import { NgxSpinnerModule, NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-children-excel-upload',
  imports: [NgFor, NgIf, ReactiveFormsModule, HttpClientModule, NgxSpinnerModule],
  templateUrl: './children-excel-upload.component.html',
  styleUrl: './children-excel-upload.component.css',
  standalone: true
})
export class ChildrenExcelUploadComponent implements OnInit {
  flagNameofRows: Boolean = true;
  nameOfRows!: FormGroup
  selectedFile!: File;
  searchResult: SearchResult[] = []
  @ViewChild('popupContainer', { read: ViewContainerRef }) popupContainer!: ViewContainerRef;

  constructor(private formBuilder: FormBuilder, private childrenExcelUploadService: ChildrenExcelUploadService, private spiner: NgxSpinnerService) { }
  ngOnInit(): void {
    this.nameOfRows = this.formBuilder.group({
      phone: ['', Validators.required],
      fatherPhone: ['', Validators.required],
      houseNumber: [''],
      firstName: [''],
      lastName: ['',],
      fatherName: [''],
      class: [''],
      address: [''],
    });
  }
  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
    }
  }

  onSubmit(): void {
    if (!this.selectedFile) {
      Swal.fire({
        icon: "error",
        title: "...אופס",
        text: "יש לבחור קובץ אקסל",
        confirmButtonText: "בסדר",
        confirmButtonColor: "#4a90e2",
      });
      return;
    }
    const formData = new FormData();
    this.spiner.show()
    formData.append('file', this.selectedFile);
    formData.append('phone', this.nameOfRows.value.phone || '');
    formData.append('fatherPhone', this.nameOfRows.value.fatherPhone || '');
    formData.append('houseNumber', this.nameOfRows.value.houseNumber || '');
    formData.append('firstName', this.nameOfRows.value.firstName || '');
    formData.append('lastName', this.nameOfRows.value.lastName || '');
    formData.append('fatherName', this.nameOfRows.value.fatherName || '');
    formData.append('className', this.nameOfRows.value.class || '');
    formData.append('address', this.nameOfRows.value.address || '');
    if ((!this.nameOfRows.value.phone || this.nameOfRows.value.phone.trim() === '') &&
      (!this.nameOfRows.value.fatherPhone || this.nameOfRows.value.fatherPhone.trim() === '')) {
      Swal.fire({
        icon: "error",
        title: "...אופס",
        text: "יש להשלים טלפון או נייד",
        confirmButtonText: "בסדר",
        confirmButtonColor: "#4a90e2",
      });
      return;
    }
    this.childrenExcelUploadService.GetResultByRows(formData).
      subscribe(result => { this.searchResult = result, this.toggleFlagNameofRows(), this.spiner.hide() });

  }
  openGraduateDialog(graduate: Graduate): void {
    this.popupContainer.clear();
    const componentRef = this.popupContainer.createComponent(GraduateComponent);
    if (graduate.idNumber)
      componentRef.instance.idGraduate = graduate.idNumber;
  }
  openDialogYeshivaStudent(yeshiva: YeshivaStudent) {
    const componentRef = this.popupContainer.createComponent(YeshivaStudentComponent);
    if (yeshiva.idNumber)
      componentRef.instance.idStudent = yeshiva.idNumber;
  }
  toggleFlagNameofRows() {
    this.flagNameofRows = !this.flagNameofRows
  }
}
