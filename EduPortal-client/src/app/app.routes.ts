import { Routes } from '@angular/router';
import { ChildrenExcelUploadComponent } from './Components/children-excel-upload/children-excel-upload.component';
import { HomeComponent } from './Components/home/home.component';

export const routes: Routes = [
    { path: 'uploadFile', component: ChildrenExcelUploadComponent },
    { path: '', component: HomeComponent },

];
