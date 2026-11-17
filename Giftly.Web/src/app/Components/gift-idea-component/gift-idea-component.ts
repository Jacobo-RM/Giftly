import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { GiftRequest } from '../../Models/GiftRequest';

@Component({
  selector: 'app-gift-idea-component',
  imports: [RouterLink, ReactiveFormsModule],
  templateUrl: './gift-idea-component.html',
  styleUrl: './gift-idea-component.css',
})
export class GiftIdeaComponent {
  @Input() loading = false;
  @Output() submitted = new EventEmitter<GiftRequest>();

  form = new FormGroup({
    relationship: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    age: new FormControl<number | null>(null, [Validators.min(0), Validators.max(120)]),
    occasion: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    interests: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    budgetMax: new FormControl<number | null>(null, [Validators.min(1)]),
    notes: new FormControl<string | null>(null),
  });

   submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const v = this.form.getRawValue();
    const request: GiftRequest = {
      relationship: v.relationship.trim(),
      age: v.age,
      occasion: v.occasion.trim(),
      interests: v.interests.trim(),
      budgetMax: v.budgetMax,
      notes: v.notes?.trim() ? v.notes.trim() : null,
    };

    this.submitted.emit(request);
  }
}
