import { Component, EventEmitter, Inject, Output, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ContactService } from '../contact.service';
import { Contact } from '../contact.model';
import { notFutureDateValidator } from '../../validators/notFutureDateValidator';

@Component({
  selector: 'app-contact-edit-component',
  standalone:false,
  templateUrl: './contact-edit-component.component.html',
  styleUrls: ['./contact-edit-component.component.css']
})
export class ContactEditComponent implements OnInit {
  contactForm!: FormGroup;
  @Output() updated = new EventEmitter<void>();

  constructor(
    private fb: FormBuilder,
    private contactService: ContactService,
    public dialogRef: MatDialogRef<ContactEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { contact: Contact }
  ) { }

  ngOnInit(): void {
    this.contactForm = this.fb.group({
      name: [this.data.contact.name, [Validators.required, Validators.minLength(2)]],
      email: [this.data.contact.email, [Validators.required, Validators.email]],
      phone: [this.data.contact.phone, [Validators.required, Validators.pattern(/^\d{10}$/)]],
      birthDate: [
        this.formatDateForInput(this.data.contact.birthDate),
        [Validators.required, notFutureDateValidator]
      ],
      address: [this.data.contact.address, Validators.required]
    });
  }

  private formatDateForInput(date?: string | Date): string {
    return date ? new Date(date).toISOString().substring(0, 10) : '';
  }

  private validateForm(): boolean {
    if (this.contactForm.invalid) {
      this.contactForm.markAllAsTouched();
      return false;
    }
    return true;
  }

  onSubmit(): void {
    if (!this.validateForm()) return;

    const updatedContact: Contact = { ...this.data.contact, ...this.contactForm.value };
    this.contactService.update(updatedContact).subscribe({
      next: () => {
        alert('Contacto actualizado con éxito');
        this.dialogRef.close(true); // Notificar al padre
      },
      error: err => {
        if (err.error?.message) {
          alert(err.error.message);
        } else {
          alert('Error al actualizar el contacto');
        }
      }
    });
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
