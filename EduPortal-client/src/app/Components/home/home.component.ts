import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  constructor(private router: Router) {}

  goToUploadExcel() {
    this.router.navigate(['/uploadFile']);
  }

  goToUploadYeshiva() {
    this.router.navigate(['/uploadYeshiva']);
  }

  goToUploadGraduate() {
    this.router.navigate(['/uploadGraduate']);
  }
}
