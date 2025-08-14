import { Component, EventEmitter, Output, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ContactService } from '../contact.service';
import { notFutureDateValidator } from '../../validators/notFutureDateValidator';

@Component({
  selector: 'app-contact-form',
  standalone:false,
  templateUrl: './contact-form.component.html',
  styleUrls: ['./contact-form.component.css']
})
export class ContactFormComponent implements OnInit {
  @Output() contactCreated = new EventEmitter<void>();
  contactForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private contactService: ContactService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.contactForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]],
      birthDate: ['', [Validators.required, notFutureDateValidator]],
      address: ['', Validators.required]
    });

    // Limitar input de teléfono a números
    this.contactForm.get('phone')?.valueChanges.subscribe(value => {
      const onlyNumbers = value.replace(/[^0-9]/g, '');
      if (value !== onlyNumbers) {
        this.contactForm.get('phone')?.setValue(onlyNumbers, { emitEvent: false });
      }
    });
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

    this.contactService.create(this.contactForm.value).subscribe({
      next: () => {
        alert('Contacto agregado con éxito');
        this.contactForm.reset();
        this.contactCreated.emit();
        this.router.navigate(['/contacts']);
      },
      error: err => {
        if (err.status === 409) {
          alert('Ya existe un contacto con este correo.');
        } else {
          alert('Ocurrió un error al guardar el contacto.');
        }
      }
    });
  }
}
