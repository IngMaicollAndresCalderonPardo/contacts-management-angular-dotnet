import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Contact } from './contact.model';

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  private apiUrl = 'https://localhost:7180/api/contacts'; // Ajusta puerto si es distinto

  constructor(private http: HttpClient) { }

  getAll(search?: string): Observable<Contact[]> {
    const url = search ? `${this.apiUrl}?search=${encodeURIComponent(search)}` : this.apiUrl;
    return this.http.get<Contact[]>(url);
  }

  getById(id: number): Observable<Contact> {
    return this.http.get<Contact>(`${this.apiUrl}/${id}`);
  }

  create(contact: Contact): Observable<Contact> {
    return this.http.post<Contact>(this.apiUrl, contact);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  update(contact: Contact): Observable<void> {
    const payload = {
      name: contact.name,
      email: contact.email,
      phone: contact.phone,
      birthDate: contact.birthDate,
      address: contact.address
    };
    return this.http.put<void>(`${this.apiUrl}/${contact.id}`, payload);

  }

}
