import { Injectable } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';

@Injectable({ providedIn: 'root' })
export class NotificationService {
  constructor(
    private confirmationService: ConfirmationService,
    private messageService: MessageService
  ) {}

  confirm(message: string, acceptCallback: () => void) {
    this.confirmationService.confirm({
      message: message,
      header: 'Confirmación',
      icon: 'pi pi-exclamation-triangle',
      accept: acceptCallback,
    });
  }

  success(summary: string, detail?: string) {
    this.messageService.add({ severity: 'success', summary, detail });
  }

  info(summary: string, detail?: string) {
    this.messageService.add({ severity: 'info', summary, detail });
  }

  warn(summary: string, detail?: string) {
    this.messageService.add({ severity: 'warn', summary, detail });
  }

  error(summary: string, detail?: string) {
    this.messageService.add({ severity: 'error', summary, detail });
  }
}
