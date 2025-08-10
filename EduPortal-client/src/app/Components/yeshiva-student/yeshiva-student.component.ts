import { Component, Input, OnInit } from '@angular/core';
import { YeshivaStudent } from '../../Models/YeshivaStudent';
import { YeshivaStudentService } from '../../Services/yeshiva-student.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-yeshiva-student',
  imports: [],
  templateUrl: './yeshiva-student.component.html',
  styleUrl: './yeshiva-student.component.css'
})

export class YeshivaStudentComponent implements OnInit {

  @Input()
  idStudent!: string;

  student!: YeshivaStudent;

  constructor(private yeshivaStudentService: YeshivaStudentService) { }

  ngOnInit(): void {
    this.yeshivaStudentService.GetYeshivaStudentById(this.idStudent).subscribe(res => {
      this.student = res;
      const s = this.student;
      const green = 'style="color: #4a90e2;"';
      const htmlContent = `
        ${s.idNumber ? `<p><b ${green}>ת"ז:</b> ${s.idNumber}</p>` : ''}
        ${s.phone ? `<p><b ${green}>טלפון:</b> ${s.phone}</p>` : ''}
        ${s.address ? `<p><b ${green}>כתובת:</b> ${s.address}</p>` : ''}
        ${s.entryDate ? `<p><b ${green}>תאריך כניסה:</b> ${s.entryDate}</p>` : ''}
        ${s.status ? `<p><b ${green}>סטטוס:</b> ${s.status}</p>` : ''}
      `;

      Swal.fire({
        title: `<span style="color: #4a90e2;">${s.fullName || ''}</span>`,
        html: htmlContent,
        width: 600,
        confirmButtonText: 'סגור',
        customClass: {
          popup: 'swal-wide'
        },
        didRender: () => {
          const confirmBtn = document.querySelector('.swal2-confirm') as HTMLElement;
          if (confirmBtn) {
            confirmBtn.style.backgroundColor = '#4a90e2';
            confirmBtn.style.color = 'white';
            confirmBtn.style.border = 'none';
            confirmBtn.style.boxShadow = 'none';
          }
        }
      });
    });
  }
}

