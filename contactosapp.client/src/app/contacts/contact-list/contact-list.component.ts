import { Component, OnInit } from '@angular/core';
import { ContactService } from '../contact.service';
import { Contact } from '../contact.model';
import { MatDialog } from '@angular/material/dialog';
import { ContactEditComponent } from '../contact-edit-component/contact-edit-component.component';

@Component({
  selector: 'app-contact-list',
  standalone: false,
  templateUrl: './contact-list.component.html',
  styleUrl: './contact-list.component.css'
})
export class ContactListComponent implements OnInit {
  contacts: Contact[] = [];
  searchTerm = '';

  constructor(private contactService: ContactService, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadContacts();
  }

  loadContacts(): void {
    this.contactService.getAll(this.searchTerm).subscribe(data => {
      // Ordenar por fecha de creación descendente (más reciente primero)
      this.contacts = data.sort((a, b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime());
    });
  }

  deleteContact(id?: number): void {
    if (!id) return;
    if (confirm('¿Seguro que quieres eliminar este contacto?')) {
      this.contactService.delete(id).subscribe(() => {
        this.loadContacts();
      });
    }
  }

  openEditDialog(contact: Contact) {
    const dialogRef = this.dialog.open(ContactEditComponent, {
      width: '600px',
      data: { contact: contact }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadContacts(); // Recarga la lista solo si hubo actualización
      }
    });
  }
}
