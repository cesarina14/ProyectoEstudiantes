import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DesignerService {
  private _preset = signal<string | null>(null); 
  preset = this._preset.asReadonly();

  setPreset(preset: string) {
    this._preset.set(preset);
  }

  clearPreset() {
    this._preset.set(null);
  }
}
