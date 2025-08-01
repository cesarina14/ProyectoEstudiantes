import { Identity } from "../../model/Identity";

export class Grades extends Identity{
   StudentId :number =0;
   SubjectTeacherId : number =0;
   score : number =0;
   ScoreLabel : string='';


  static JsonInstance(entity: any): Grades {
   return entity ? Object.assign(new Grades(), entity ): new Grades();

  }

  static JsonInstaces(entity: any[]): Grades[] {
    return entity.map(item => Grades.JsonInstance(item));
  }

}