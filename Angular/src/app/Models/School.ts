import { Principal } from "./Principal";
import { SchoolType } from "./SchoolType";
import { Subject } from "./Subject";

export class School {
        public SchoolID!: number;
        public Name!: string;
        public Address!: string;
        public LgaID!: number;
        public ShowPosition!: boolean;
        public Append!: string;
        public ProprietorID!: string;
        public Type! : SchoolType;
        public SsCompulsories!: number[];
        public SsDrops!: number[];
}

export class SchoolModel
    {
       public school!: School 
       public principal!: Principal 
    }


