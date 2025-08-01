import { Identity } from "../../model/Identity";
import { Grades } from "./grades";
import { SubjectStudent } from "./subject-student";

export class Student extends Identity{
   Name :string ='';
   Phone : string ='';
   Gender : string='';
   LastName :string =''
   Age : number =0;
   Tutor : string='';
   TutorRelationShip :string =''
   TutorPhone :string =''
   SubjectStudentList : SubjectStudent[]=[];
   CreatedAt : Date = new Date();
   CreateBy : string ='';

/**
 *
 */
constructor(init?: Partial<Student>) {
    super();
    Object.assign(this, init);
  }

  static JsonInstance(entity: any): Student {
   var _object =   entity ? Object.assign(new Student(), entity): new Student();
    _object.SubjectStudentList = SubjectStudent.JsonInstaces(entity?.SubjectStudentList);
    return _object;

  }

  static JsonInstaces(entity: any[]): Student[] {
    return entity.map(item => Student.JsonInstance(item));
  }

}