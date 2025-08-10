import { Component, Input, OnInit } from '@angular/core';
import { Graduate } from '../../Models/Graduate';
import { GraduateService } from '../../Services/graduate.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-graduate',
  templateUrl: './graduate.component.html',
  styleUrls: ['./graduate.component.css']
})
export class GraduateComponent implements OnInit {

  @Input()
  idGraduate!: string;
  graduate!: Graduate;

  constructor(private graduateService: GraduateService) { }

  ngOnInit(): void {
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
}}
