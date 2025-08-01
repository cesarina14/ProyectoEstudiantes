import { ChangeDetectorRef, Component, inject, Inject, OnInit } from '@angular/core';
import { Student } from '../model/student';
import { StudentService } from '../service/student.service';
import {
  DialogService,
  DynamicDialogConfig,
  DynamicDialogModule,
  DynamicDialogRef,
} from 'primeng/dynamicdialog';
import { MessageService, SelectItem } from 'primeng/api';
import { FormsModule } from '@angular/forms';

import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { Subject } from '../model/subject-enum';
import { TableModule } from 'primeng/table';
import { SubjectStudent } from '../model/subject-student';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { Helper } from '../helper';
import { Teacher } from '../model/teacher.enum';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { FieldsetModule } from 'primeng/fieldset';
import { Trismestre } from '../model/trismestres-enum';
import { TutorRelationShipEnum } from '../model/tutor-relationship-enum';
import { Year } from '../model/year-enum';
import { Message } from 'primeng/message';
import { MessageModule } from 'primeng/message';
import { ToastModule } from 'primeng/toast';
@Component({
  selector: 'app-student-component',
  templateUrl: './student.component.html',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    ButtonModule,
    TableModule,
    DialogModule,
    InputTextModule,
    ConfirmDialogModule,
    FieldsetModule,
    MessageModule,
    ToastModule
  ],
  providers: [MessageService,Message],
})
export class StudentComponent implements OnInit {
  public Student: Student = new Student();

  public activeTab = 0;
  public DisplayDialog: boolean = false;
  public SubjectList: SelectItem<any>[] = [];
  public TeacherList: SelectItem<any>[] = [];
  public TrimestreList: SelectItem<any>[] = [];
  public TutorRelationList: SelectItem<any>[] = [];
  public YearList: SelectItem<any>[] = [];

  public SubjectDialog: boolean = false;
  public SelectSubject: number = 0;

  public SubjectStudent: SubjectStudent = new SubjectStudent();
  public SubjectStudentList: SubjectStudent[] = [];

  public ModoFormulario: string | null = null;
  public editandoIndex: number | null = 0;
  public SelectedTeacherId: number | null = null;
  public SelectedSubjectId: number | null = null;
  public SelectedYearId: string | null = '';

  /// MESSAGE 
  public DisplayMessage :boolean =false;
  public Message:string ="";

  constructor(
    private readonly _StudentService: StudentService,
    public _Ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private cdr: ChangeDetectorRef,
    private _messageService :MessageService
  ) {}
  async ngOnInit(): Promise<void> {
    this.SubjectList = Helper.enumToSelectItems(Subject);
    this.TeacherList = Helper.enumToSelectItems(Teacher);
    this.TrimestreList = Helper.enumToSelectItems(Trismestre);
    this.TutorRelationList = Helper.enumToSelectItems(TutorRelationShipEnum);
    this.YearList = Helper.enumToSelectItems(Year);

    this.Student = this.config?.data?.Student;
    if (this.Student?.Id > 0) {
      this._StudentService.GetById(this.Student.Id).subscribe((_response) => {
        this.Student = _response.Value;
        this.SubjectStudentList = _response.Value.SubjectStudentList;
    
      });
    } else {
      this.Student = new Student();
    }
  }
  public getSubject(value: any) {
    return this.SubjectList.find((r) => r.value == value)?.label;
  }
  public getTrimestre(value: any) {
    return this.TrimestreList.find((r) => r.value == value)?.label;
  }
  public getTeacher(value: any) {
    return this.TeacherList.find((r) => r.value == value)?.label;
  }
  public getLiteral(value: any) {
    if (value >= 90 && value <= 100) {
      return 'A';
    } else if (value >= 80 && value <= 89) {
      return 'B';
    } else if (value >= 70 && value <= 79) {
      return 'C';
    } else if (value >= 60 && value <= 69) {
      return 'D';
    } else {
      return 'F';
    }
  }

  public onSubmit() {
    if (this.Student.Name === '') {
      this.warning('Nombre es requerido');
      return;
    }
    if (this.Student.LastName === '') {
      this.warning('Apellido es requerido');
      return;
    }
    if (this.Student.Age ===0) {
      this.warning('Edad no es valida');
      return;
    }
    this.Student.SubjectStudentList = this.SubjectStudentList;
    if(this.Student.Id >0) {
      this._StudentService.Update(this.Student).subscribe(_response => {
        if(_response.Success) {
          this._Ref?.close(true);
        }
      });
    }
    else {
      this._StudentService.Create(this.Student).subscribe(_response => {
        if(_response.Success) {
          this._Ref?.close(true);
        }
      });
    }
  }


  public showSubject(modo: 'editar' | 'agregar', index?: number) {
    this.ModoFormulario = modo;
    this.editandoIndex = index ?? null;
    if (modo === 'editar' && index !== undefined) {
      const fila = this.SubjectStudentList[index];
      this.SubjectStudent = { ...fila };
      this.SelectedTeacherId = fila.TeacherId;
      this.SelectedSubjectId = fila.SubjectId;
      this.SelectedYearId = this.YearList.find(r=> r.value == fila?.Year?.trim().replace(",",""))?.value;
    
    } else {
      this.SelectedTeacherId =  null;
      this.SelectedSubjectId = null;
      this.SelectedYearId = null;
      this.SubjectStudent = new SubjectStudent();
    }
  }

  public saveSubject() {
   
    const exists = this.SubjectStudentList.some(s =>
      s.SubjectId?.toString() === this.SubjectStudent.SubjectId?.toString() 
      && s.Trimestre.toString() === this.SubjectStudent.Trimestre.toString() 
      && s.Year?.toString().trim() === this.SelectedYearId?.toString().trim() && this.ModoFormulario === 'agregar')
    
    if (exists) {
      this.DisplayWarning("No se puede agregar la asignatura dos veces el mismo trismetre y año, favor de validar!")
          setTimeout(() => {
            this.HideWarning();
          }, 2000);
  
      return;
    }
    
    if (this.ModoFormulario === 'agregar') {
      this.SubjectStudent.TeacherId = this.SelectedTeacherId;
      this.SubjectStudent.StudentId = this.Student.Id;
      this.SubjectStudent.Year = this.SelectedYearId?.toString() ?? null;

      this.SubjectStudentList.push({ ...this.SubjectStudent });
   
    } else if ( this.ModoFormulario === 'editar' && this.editandoIndex !== null) 
    {
      this.SubjectStudentList[this.editandoIndex] = { ...this.SubjectStudent };
    }
    this.cancelled();
  }

 public cancelled() {
    this.ModoFormulario = null;
    this.editandoIndex = null;
    this.SubjectStudent = new SubjectStudent();
  }

  removeRow(index: number) {
    var row = this.SubjectStudentList[index];
    if(row){
      this.SubjectStudentList.splice(index);
    }
  }

  public onClose(): void {
    this._Ref?.close();
  }
  public IsSubjectActive() {
    return this.activeTab === 1 && this.Student.Id > 0;
  }
  private DisplayWarning(msj : string) {
    this.Message = msj;
    this.DisplayMessage = true;
  }
  private HideWarning() {
    this.DisplayMessage =false ;
    this.cdr.detectChanges(); 
  }

  warning(error:string) {
    this._messageService.add({
      severity: 'warning',
      summary: 'Warning',
      detail: error,
    });
  }
}
