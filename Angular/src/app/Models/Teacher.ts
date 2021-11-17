import { Gender } from "./Gender";
import { School } from "./School";

export class Teacher {
    public FirstName!: string;
    public LastName!: string;
    public Address!: string;
    public Email!: string;
    public SchoolID!: number;
    public LgaID!: number;
    public Gender = Gender;
    public PasswordHash!: string;
    public TeacherSubjects!: number[];
    
  
}

export class ReturnTeacher{
    public Id!: string;
    public FirstName! : string;
    public LastName!: string;
    public Eamil!: string;
    public SchoolID!: number;
    public Gender!: number;
    public PhoneNumber!: string;
    public TeacherSubjects!: any[];
  }