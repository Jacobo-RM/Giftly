export interface GiftRequest {
  relationship: string;
  age: number | null;
  occasion: string;
  interests: string;
  budgetMax: number | null;
  notes: string | null;
}