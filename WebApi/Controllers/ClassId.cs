public class ClassId{
    public int schoolID;
}

public class SClassId{
    public int schoolID;
    public int ClassArmID { get; set; }
}

public class FSID{
    public int schoolID;
    public string searchQuery { get; set; }
}

public class SID{
    public int schoolID { get; set; }
    public int SubjectID { get; set; }
}

public class TID{
    public int schoolID { get; set; }
    public string Id { get; set; }
}

public class TermID{
    public int Id { get; set; }
    
}

public class SE {
    public int ClassSubjectID { get; set; }
    public string StudentID { get; set; }
}

public class SO {
    public int SchoolID { get; set; }
}



    
