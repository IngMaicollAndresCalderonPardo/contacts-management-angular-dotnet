export interface Contact {
  id?: number;
  name: string;
  email?: string;
  phone?: string;
  birthDate?: string; // formato ISO
  address?: string;
  createdAt: string;
}
