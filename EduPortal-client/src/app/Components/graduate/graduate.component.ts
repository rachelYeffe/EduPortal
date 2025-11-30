import { Component, Input, OnInit } from '@angular/core';
import { Graduate } from '../../Models/Graduate';
import { GraduateService } from '../../Services/graduate.service';
import Swal from 'sweetalert2';
import { NgxSpinnerModule, NgxSpinnerService } from 'ngx-spinner';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-graduate',
  standalone: true,
  imports: [NgIf, NgxSpinnerModule],
  templateUrl: './graduate.component.html',
  styleUrls: ['./graduate.component.css']
})
export class GraduateComponent implements OnInit {

  @Input()
  idGraduate!: string;
  graduate!: Graduate;
  selectedFile!: File;
  constructor(private graduateService: GraduateService, private spiner: NgxSpinnerService) { }

  ngOnInit(): void {

  }
  getGraduate() {
    this.graduateService.GetGraduateById(this.idGraduate).subscribe(res => {
      this.graduate = res;

      const g = this.graduate;
      const green = 'style="color: #4a90e2;"';

      const htmlContent = `
        ${g.idNumber ? `<p><b ${green}>ת"ז:</b> ${g.idNumber}</p>` : ''}
        ${g.accountNumber ? `<p><b ${green}>מס' חשבון:</b> ${g.accountNumber}</p>` : ''}
        ${g.institution ? `<p><b ${green}>מוסד:</b> ${g.institution}</p>` : ''}
        ${g.mobilePhone ? `<p><b ${green}>טלפון נייד:</b> ${g.mobilePhone}</p>` : ''}
        ${g.homePhone ? `<p><b ${green}>טלפון בבית:</b> ${g.homePhone}</p>` : ''}
        ${g.addHomePhone ? `<p><b ${green}>טלפון נוסף בבית:</b> ${g.addHomePhone}</p>` : ''}
        ${g.passport ? `<p><b ${green}>דרכון:</b> ${g.passport}</p>` : ''}
        <hr>
        ${(g.street || g.houseNumber || g.apartment || g.entrance || g.city) ?
          `<p><b ${green}>כתובת:</b> ${g.street || ''} ${g.houseNumber || ''} ${g.apartment ? 'דירה ' + g.apartment : ''} ${g.entrance ? 'כניסה ' + g.entrance : ''} ${g.city || ''}</p>` : ''}
        <hr>
        ${(g.fatherStreet || g.fatherHouseNumber || g.fatherApartment || g.fatherEntrance || g.fatherCity) ?
          `<p><b ${green}>כתובת אב:</b> ${g.fatherStreet || ''} ${g.fatherHouseNumber || ''} ${g.fatherApartment ? 'דירה ' + g.fatherApartment : ''} ${g.fatherEntrance ? 'כניסה ' + g.fatherEntrance : ''} ${g.fatherCity || ''}</p>` : ''}
        ${g.fatherPhone ? `<p><b ${green}>טלפון אב:</b> ${g.fatherPhone}</p>` : ''}
        ${g.fatherBusinessPhone ? `<p><b ${green}>טלפון בעבודה אב:</b> ${g.fatherBusinessPhone}</p>` : ''}
        ${g.addFatherBusinessPhone ? `<p><b ${green}>טלפון נוסף עבודה אב:</b> ${g.addFatherBusinessPhone}</p>` : ''}
        <hr>
        ${g.mail ? `<p><b ${green}>מייל:</b> ${g.mail}</p>` : ''}
        ${g.kind ? `<p><b ${green}>סוג:</b> ${g.kind}</p>` : ''}
        ${g.cycle ? `<p><b ${green}>מחזור:</b> ${g.cycle}</p>` : ''}
        ${g.age ? `<p><b ${green}>גיל:</b> ${g.age}</p>` : ''}
        ${g.status ? `<p><b ${green}>סטטוס:</b> ${g.status}</p>` : ''}
      `;

      Swal.fire({
        title: `<span style="color: #4a90e2;">${g.firstName || ''} ${g.lastName || ''}</span>`,
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
    })
  }
  uploadFile(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
    }
  }
submitGraduate() {
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
  formData.append('file', this.selectedFile);

  this.spiner.show();

  this.graduateService.addGradute(formData).subscribe({
    next: (res: string) => {
      this.spiner.hide();

      Swal.fire({
        icon: "success",
        title: "הקובץ הועלה בהצלחה",
        text: res,
        confirmButtonText: "חזור לעמוד הבית",
        confirmButtonColor: "#4a90e2",
        showDenyButton: true,
        denyButtonText: "העלה קובץ נוסף",
        denyButtonColor: "#4a90e2",
      }).then((result) => {
        if (result.isConfirmed) {
          window.location.href = '/';  
          } else if (result.isDenied) {
          this.selectedFile = undefined as any;
          const fileInput = document.querySelector('input[type="file"]') as HTMLInputElement;
          if (fileInput) fileInput.value = '';
        }
      });
    },
    error: (err) => {
      this.spiner.hide();
      Swal.fire({
        icon: "error",
        title: "שגיאה בהעלאה",
        text: err.error || "התרחשה שגיאה, נסי שוב",
        confirmButtonColor: "#4a90e2",
      });
    }
  });
}


}