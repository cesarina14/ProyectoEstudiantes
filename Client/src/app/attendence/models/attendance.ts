import { Identity } from "../../../model/Identity";

export class Attendence extends Identity{
StudentId : number =0;
Date : Date = new Date();
Present: boolean=false;
CreateAt : Date = new Date();
CreateBy : string ='';
constructor(init?: Partial<Attendence>) {
    super();
    Object.assign(this, init);
  }

  static JsonInstance(entity: any): Attendence {
   return  entity ? Object.assign(new Attendence(), entity ): new Attendence();


  }

  static JsonInstaces(entity: any[]): Attendence[] {
    return entity.map(item => Attendence.JsonInstance(item));
  }

}