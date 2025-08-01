import { AfterViewInit, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Student } from '../../../Student/model/student';
import { AttendenceService } from '../service/attendence.service';
import { Attendence } from '../models/attendance';
import { Subject } from '../../../Student/model/subject-enum';
import { StudentService } from '../../../Student/service/student.service';
import { TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { Helper } from '../../../Student/helper';
import { DialogModule } from 'primeng/dialog';
import { DatePickerModule } from 'primeng/datepicker';
import { firstValueFrom } from 'rxjs';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
export class AttendanceWithStudent {
  Id : number =0;
  StudentId: number = 0;
  StudentName: string = '';
  IsPresent: boolean = false;
  Date :Date = new Date();
  CreatedAt  :Date = new Date();
}

@Component({
  selector: 'app-attendance',
  templateUrl: './attendence.component.html',
  styleUrls: ['./attendence.component.scss'],
  imports: [
    CommonModule,
    TableModule,
    FormsModule,
    ButtonModule,
    InputTextModule,
    DatePickerModule,
    ToastModule,
    CommonModule,
    DialogModule

  ],
  standalone: true,
  providers: [MessageService, DialogService],

})
export class AttendanceComponent implements OnInit {
  public Attendance: Attendence = new Attendence();
  private  _studentList: Student[] = [];
  public AttendanceList: Attendence[] = [];
  public AttendanceFilterList: Attendence[] = [];
  public SubjectList: { key: string; Value: number }[] = [];
  public selectedDate: Date = new Date();
  public editingRows: { [key: string]: boolean } = {};
  public AttendanceWithStudentList: AttendanceWithStudent[] = [];
  public AttendanceWithStudentTable: AttendanceWithStudent[] = [];
  public ShowDialog: boolean = false;
  public Editable :boolean =false;


  constructor(
    private _service: AttendenceService,
    private _StudentService: StudentService,
    private _cdr: ChangeDetectorRef,
    private _messageService :MessageService
  ) {}

  public async ngOnInit(): Promise<void> {
    await this.loadStudentList();
    this._StudentService.ListToAttendence().subscribe((_response) => {
      if (_response.Success) {
        const studentList = _response.Value;
        if (studentList.length > 0) {
            studentList.forEach((_element) => {
            var _newElement = new AttendanceWithStudent();
            _newElement.IsPresent = false;
            _newElement.StudentId = _element.Id;
            _newElement.StudentName = `${_element.Name} ${_element.LastName}`;

            this.AttendanceWithStudentList.push(_newElement);
          });
        }
      }});
       this.loadAttendanceList();
        this._cdr.markForCheck();
        this._cdr.detectChanges();
  }

  public ShowModal(): void {
    this.ShowDialog = true;
  }
  public Hide(): void {
    this.ShowDialog = false;
  }
  public registerAttendance(record: Attendence): void {
    this.AttendanceList.push(record);
  }
  public getAttendanceByDate(date: Date): void {
    this.AttendanceFilterList = this.AttendanceList.filter(
      (record) => record.Date.toDateString() === date.toDateString()
    );
  }

  public async Save() {
    this.AttendanceList=[];
    this.AttendanceWithStudentList.map(_element=> {
     const _newAttendence = new Attendence ();
        _newAttendence.Id = _element.Id;
       _newAttendence.Present = _element.IsPresent;
       _newAttendence.StudentId = _element.StudentId;
       _newAttendence.CreateAt = _element.CreatedAt;
       this.AttendanceList.push(_newAttendence)

    });
    if(this.AttendanceList.length > 0) {
      if(this.Editable) {
        const id = this.AttendanceWithStudentList[0]?.Id
        const _newAttendencToEdit =  this.AttendanceList.filter(r=> r.Id == id)[0];
        this._service.Update(_newAttendencToEdit).subscribe(async _response => {
          if(_response.Success) {
            this.ShowDialog =false;
            this.success();
            await this.loadStudentList();
            await this.loadAttendanceList();
            this._cdr.detectChanges();
  
      
          }
          else {
            this.warning();
            console.log(_response.Code,_response.Message);
          }
       })
      }
      else {
        this._service.Create(this.AttendanceList).subscribe(async _response => {
          if(_response.Success) {
            this.ShowDialog =false;
            this.success();
            await this.loadStudentList();
            await this.loadAttendanceList();
            this._cdr.detectChanges();
      

          }
          else {
            this.warning();
            console.log(_response.Code,_response.Message);
          }
       })
      }
    
    }
  }
  private async loadAttendanceList(): Promise<void> {
      const attendenceResponse = await firstValueFrom(this._service.List());
  
      if (attendenceResponse.Success) {
        this.AttendanceWithStudentTable = [];
  
        attendenceResponse.Value.forEach(_studentElement => {
          const _student = this._studentList.find(r => r.Id === _studentElement.StudentId);
  
          const _newElement = new AttendanceWithStudent();
          _newElement.Id = _studentElement.Id;
          _newElement.IsPresent = _studentElement.Present;
          _newElement.StudentId = _studentElement.StudentId;
          _newElement.StudentName = `${_student?.Name ?? ''} ${_student?.LastName ?? ''}`;
  
          this.AttendanceWithStudentTable.push(_newElement);
        });
  
        this._cdr.markForCheck(); // Solo una vez es suficiente
      }
}
  private async loadStudentList() :Promise<void> {
    const studentResponse = await firstValueFrom(this._StudentService.List());
  
    if (studentResponse.Success && studentResponse.Value.length > 0) {
      this._studentList = studentResponse.Value;
    }

  }
  public EditAttendence(index:number) :void 
  {
       this.ShowDialog =true;
       this.AttendanceWithStudentList= [];
       this.AttendanceWithStudentList.push(this.AttendanceWithStudentTable[index]);
       this.Editable = true;
  }
  success() {
    this._messageService.add({
      severity: 'success',
      summary: 'Sucesss',
      detail: 'Se ha realizado la acción exitosamente',
    });
}
warning() {
  this._messageService.add({
    severity: 'success',
    summary: 'Warning',
    detail: 'Se ha realizado un error favor de validar',
  });
}
}
