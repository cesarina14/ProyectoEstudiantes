import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  private _transitionComplete = signal(false);

  transitionComplete = this._transitionComplete.asReadonly();

  markTransitionComplete() {
    this._transitionComplete.set(true);
  }
}
