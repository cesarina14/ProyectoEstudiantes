import {AfterViewInit,ChangeDetectorRef,Component,OnInit} from '@angular/core';
import { Student } from '../../model/student';
import { StudentService } from '../../service/student.service';
import { TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { DialogModule } from 'primeng/dialog';
import {DialogService,DynamicDialogModule,DynamicDialogRef,} from 'primeng/dynamicdialog';
import { StudentComponent } from '../student.component';
import { FormsModule } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
@Component({
  selector: 'app-student-component-list',
  templateUrl: './student.component-list.html',
  styleUrls: ['./student.component-list.scss'],
  imports: [
    CommonModule,
    TableModule,
    DialogModule,
    FormsModule,
    ToastModule,
    DynamicDialogModule
  ],
  standalone: true,
  providers: [MessageService, DialogService],
})
export class StudentListComponent implements OnInit, AfterViewInit {
  public StudentList: Student[] = [];
  DisplayDialog: boolean = false;
  private _Ref!: DynamicDialogRef;

  public displayConfirmDialog = false;
  private _StudentIdToDelete :number | null =null;
  constructor(
    private _Service: StudentService,
    private _DialogService: DialogService,
    private _MessageService: MessageService,
    private _cdr: ChangeDetectorRef
  ) {}

  public async ngOnInit(): Promise<void> {

    await this.updateList();
  }
  public ngAfterViewInit(): void {
    this._cdr.detectChanges();
  }

  public Add(): void {
    this._Ref = this._DialogService.open(StudentComponent, {
      header: 'Nuevo Estudiante',
      width: '70%',
      contentStyle: { 'max-height': '500px', overflow: 'auto' },
      data: { Student: new Student() },
    });

    this._Ref.onClose.subscribe(async (result) => {
      if (result) {
        await this.updateList();
        this.success();
      }
    });
  }
  public Edit(selected: Student): void {
    this._Ref = this._DialogService.open(StudentComponent, {
      header: selected?.Id > 0 ? `Estudiante # ${selected.Id}` : 'Nuevo Estudiante',
      width: '70%',
      contentStyle: { 'max-height': '500px', overflow: 'auto' },
      data: { Student: selected },
    });

    this._Ref.onClose.subscribe(async (result) => {
      if (result) {
       await this.updateList();
        this.success();
      }
    });
  }

  public onDelete(index: number): void {
    const _student = this.StudentList[index];
    if(_student.Id>0) {
      this.displayConfirmDialog = true;
      this._StudentIdToDelete = _student.Id;
    }
    
  }
  public onConfirmDelete():void {
    if(this._StudentIdToDelete) {
      this._Service.Remove(this._StudentIdToDelete).subscribe(async response => {
        if (response.Success) {
          await this.updateList();
          this.success();
          this.displayConfirmDialog = false;
          this._StudentIdToDelete = null;
          
        }
      });
    }
    
    }
    
    onCancelDelete() {
      this.displayConfirmDialog = false;
    
    }
  success() {
    this._MessageService.add({
      severity: 'success',
      summary: 'Sucesss',
      detail: 'Se ha realizado la acción exitosamente',
    });
}
///modal
showModal() {
  this.DisplayDialog = true;
}

hideModal() {
  this.DisplayDialog = false;
}

private async updateList() : Promise<void>{
  this._Service.List().subscribe((_response) => {
    if (_response.Success) {
      this.StudentList = _response.Value;
      this._cdr.detectChanges();
    }
  })
}

}
