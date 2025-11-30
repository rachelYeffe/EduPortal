import { Routes } from '@angular/router';
import { ChildrenExcelUploadComponent } from './Components/children-excel-upload/children-excel-upload.component';
import { HomeComponent } from './Components/home/home.component';
import { YeshivaStudentComponent } from './Components/yeshiva-student/yeshiva-student.component';
import { GraduateComponent } from './Components/graduate/graduate.component';

export const routes: Routes = [
    { path: 'uploadFile', component: ChildrenExcelUploadComponent },
    { path: '', component: HomeComponent },
    { path: 'uploadYeshiva', component: YeshivaStudentComponent },
    { path: 'uploadGraduate', component: GraduateComponent },

];
