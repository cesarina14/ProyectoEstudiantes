
import { Identity } from "../../model/Identity";

export class SubjectStudent extends Identity{
   StudentId :number | null = null ;
   SubjectId : number | null = null;
   Score : number =0;
   ScoreLabel : string='';
   TeacherId :number | null = null;
   Trimestre : string ='';
   Year : string | null = '';
   Date : Date = new Date();



  static JsonInstance(entity: any): SubjectStudent {
   return entity ? Object.assign(new SubjectStudent(), entity ): new SubjectStudent();

  }

  static JsonInstaces(entity: any[]): SubjectStudent[] {
    return entity.map(item => SubjectStudent.JsonInstance(item));
  }

}