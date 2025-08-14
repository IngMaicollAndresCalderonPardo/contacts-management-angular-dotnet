import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactListComponent } from './contacts/contact-list/contact-list.component';
import { ContactFormComponent } from './contacts/contact-form/contact-form.component';
import { HomeComponent } from './contacts/home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'register', component: ContactFormComponent },
  { path: 'contacts', component: ContactListComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
