import { Gender } from "./Gender";

export class Student {
    public Id!: string;
    public FirstName!: string
    public LastName!: string;
    public Address!: string;
    public ClassArmID!: number;
    public SchoolID!: number;
    public Gender  = Gender;
    public RegNo!: string;
}