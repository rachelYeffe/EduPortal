import { Component, Input, OnInit } from '@angular/core';
import { YeshivaStudent } from '../../Models/YeshivaStudent';
import { YeshivaStudentService } from '../../Services/yeshiva-student.service';
import Swal from 'sweetalert2';
import { NgxSpinnerModule, NgxSpinnerService } from 'ngx-spinner';
import { NgIf } from '@angular/common';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-yeshiva-student',
  standalone: true,
  imports: [NgIf, NgxSpinnerModule],
  templateUrl: './yeshiva-student.component.html',
  styleUrls: ['./yeshiva-student.component.css']
})
export class YeshivaStudentComponent implements OnInit {

  @Input() idStudent!: string;

  student!: YeshivaStudent;
  selectedFile!: File | null;

  requiredColumns = [
    "שם ומשפחה",
    "טלפון",
    "תז",
    "כתובת",
    "תאריך כניסה",
    "סטטוס"
  ];

  constructor(
    private yeshivaStudentService: YeshivaStudentService,
    private spiner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.selectedFile = null;
  }
async uploadFile(event: Event) {
  const input = event.target as HTMLInputElement;
  if (input.files && input.files.length > 0) {
    this.selectedFile = input.files[0];

    const isValid = await this.validateColumns(this.selectedFile);
    if (!isValid) {
      this.selectedFile = null;
      input.value = "";
    }
  }
}

async validateColumns(file: File): Promise<boolean> {
  const data = await file.arrayBuffer();
  let columns: string[] = [];

  if (file.name.endsWith(".csv")) {
    let text = new TextDecoder("utf-8").decode(data);
    if (/�/.test(text)) {
      text = new TextDecoder("windows-1255").decode(data);
    }

    columns = text.split("\n")[0].split(",").map(c => c.trim());
    console.log("CSV parsed columns:", columns);
  } else {
    const workbook = XLSX.read(data);
    const firstSheet = workbook.SheetNames[0];
    const sheet = workbook.Sheets[firstSheet];
    const json = XLSX.utils.sheet_to_json(sheet, { header: 1 });
    columns = json[0] as string[];
  }

  const missing = this.requiredColumns.filter(col => !columns.includes(col));

  if (missing.length > 0) {
    Swal.fire({
      icon: "error",
      title: "הקובץ אינו תואם",
      html: `<p>העמודות הבאות חסרות בקובץ:</p><b>${missing.join("<br>")}</b>`,
      confirmButtonColor: "#4a90e2"
    });
    return false;
  }

  Swal.fire({
    icon: "success",
    title: "הקובץ מאומת",
    text: "כל העמודות הנדרשות קיימות",
    confirmButtonColor: "#4a90e2"
  });

  return true;
}

  submitYeshiva() {
    if (!this.selectedFile) {
      Swal.fire({
        icon: 'error',
        title: '...אופס',
        text: 'יש לבחור קובץ אקסל או CSV',
        confirmButtonText: 'בסדר',
        confirmButtonColor: '#4a90e2'
      });
      return;
    }

    const formData = new FormData();
    formData.append('file', this.selectedFile);

    this.spiner.show();

    this.yeshivaStudentService.addYeshiva(formData).subscribe({
      next: (res: number) => {
        this.spiner.hide();
        Swal.fire({
          icon: 'success',
          title: 'הקובץ הועלה בהצלחה',
          html: `אברכים נוספו למערכת בהצלחה <span style="font-size:24px; font-weight:bold;"> ${res}</span>`,
          confirmButtonText: 'חזור לעמוד הבית',
          confirmButtonColor: '#4a90e2',
          showDenyButton: true,
          denyButtonText: 'העלה קובץ נוסף',
          denyButtonColor: '#4a90e2'
        }).then((result) => {
          if (result.isConfirmed) {
            window.location.href = '/';
          } else if (result.isDenied) {
            this.selectedFile = null;
            const fileInput = document.querySelector('input[type="file"]') as HTMLInputElement;
            if (fileInput) fileInput.value = '';
          }
        });
      },
      error: (err) => {
        this.spiner.hide();
        Swal.fire({
          icon: 'error',
          title: 'שגיאה בהעלאה',
          text: err.error || 'התרחשה שגיאה, נסי שוב',
          confirmButtonColor: '#4a90e2'
        });
      }
    });
  }

  getYeshivaStudent() {
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
        customClass: { popup: 'swal-wide' },
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
