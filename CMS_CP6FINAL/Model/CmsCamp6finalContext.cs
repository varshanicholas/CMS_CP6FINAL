using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CMS_CP6FINAL.Model;

public partial class CmsCamp6finalContext : DbContext
{
    public CmsCamp6finalContext()
    {
    }

    public CmsCamp6finalContext(DbContextOptions<CmsCamp6finalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DailyAppointmentAvailability> DailyAppointmentAvailabilities { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<DoctorAvailability> DoctorAvailabilities { get; set; }

    public virtual DbSet<DoctorReferral> DoctorReferrals { get; set; }

    public virtual DbSet<LabReport> LabReports { get; set; }

    public virtual DbSet<LabTest> LabTests { get; set; }

    public virtual DbSet<LabTestCategory> LabTestCategories { get; set; }

    public virtual DbSet<LabTestPrescription> LabTestPrescriptions { get; set; }

    public virtual DbSet<MainPrescription> MainPrescriptions { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<MedicineCategory> MedicineCategories { get; set; }

    public virtual DbSet<MedicineInventory> MedicineInventories { get; set; }

    public virtual DbSet<MedicinePrescription> MedicinePrescriptions { get; set; }

    public virtual DbSet<NewAppointment> NewAppointments { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<UserRegistration> UserRegistrations { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source =SHAJITH-NICHOLA\\SQLEXPRESS; Initial Catalog = CMS_CAMP6FINAL; Integrated Security = True; Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DailyAppointmentAvailability>(entity =>
        {
            entity.HasKey(e => e.DailyAppointmentId).HasName("PK__DailyApp__A240DF94797F1E53");

            entity.ToTable("DailyAppointmentAvailability");

            entity.HasOne(d => d.Appointment).WithMany(p => p.DailyAppointmentAvailabilities)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DailyAppo__Appoi__5BE2A6F2");

            entity.HasOne(d => d.DocAvl).WithMany(p => p.DailyAppointmentAvailabilities)
                .HasForeignKey(d => d.DocAvlId)
                .HasConstraintName("FK__DailyAppo__DocAv__5CD6CB2B");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BEDC40047A1");

            entity.ToTable("Department");

            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__Doctor__2DC00EBFD1BB3974");

            entity.ToTable("Doctor");

            entity.Property(e => e.ConsultationFee).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Specialization).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.SpecializationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Doctor__Speciali__440B1D61");

            entity.HasOne(d => d.Staff).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Doctor__StaffId__4316F928");
        });

        modelBuilder.Entity<DoctorAvailability>(entity =>
        {
            entity.HasKey(e => e.DocAvlId).HasName("PK__Doctor_A__387178EF30C22593");

            entity.ToTable("Doctor_Availability");

            entity.Property(e => e.EveningSession)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.MorningSession)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Weekdays)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.Doctor).WithMany(p => p.DoctorAvailabilities)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Doctor_Av__Docto__4AB81AF0");
        });

        modelBuilder.Entity<DoctorReferral>(entity =>
        {
            entity.HasKey(e => e.ReferralId).HasName("PK__DoctorRe__A2C4A96637DA1AE0");

            entity.Property(e => e.ReferralDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Appointment).WithMany(p => p.DoctorReferrals)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DoctorRef__Appoi__7F2BE32F");

            entity.HasOne(d => d.Patient).WithMany(p => p.DoctorReferrals)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DoctorRef__Patie__00200768");

            entity.HasOne(d => d.ReferredDoctor).WithMany(p => p.DoctorReferrals)
                .HasForeignKey(d => d.ReferredDoctorId)
                .HasConstraintName("FK__DoctorRef__Refer__7D439ABD");

            entity.HasOne(d => d.ReferringDoctor).WithMany(p => p.DoctorReferrals)
                .HasForeignKey(d => d.ReferringDoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DoctorRef__Refer__7C4F7684");

            entity.HasOne(d => d.Specialization).WithMany(p => p.DoctorReferrals)
                .HasForeignKey(d => d.SpecializationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DoctorRef__Speci__7E37BEF6");
        });

        modelBuilder.Entity<LabReport>(entity =>
        {
            entity.HasKey(e => e.LabReportId).HasName("PK__LabRepor__971DB70DF5A77ED5");

            entity.ToTable("LabReport");

            entity.Property(e => e.LabTestValue)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Remarks).HasMaxLength(255);

            entity.HasOne(d => d.Appointment).WithMany(p => p.LabReports)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK__LabReport__Appoi__03F0984C");

            entity.HasOne(d => d.LabTest).WithMany(p => p.LabReports)
                .HasForeignKey(d => d.LabTestId)
                .HasConstraintName("FK__LabReport__LabTe__02FC7413");

            entity.HasOne(d => d.LabTestPrescription).WithMany(p => p.LabReports)
                .HasForeignKey(d => d.LabTestPrescriptionId)
                .HasConstraintName("FK__LabReport__LabTe__04E4BC85");
        });

        modelBuilder.Entity<LabTest>(entity =>
        {
            entity.HasKey(e => e.LabTestId).HasName("PK__LabTest__64D339257F9ECA83");

            entity.ToTable("LabTest");

            entity.Property(e => e.Cost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.LabTestName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ReferenceMaxRange).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ReferenceMinRange).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ResultType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SampleRequired)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.HasOne(d => d.Category).WithMany(p => p.LabTests)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LabTest__Categor__4F7CD00D");
        });

        modelBuilder.Entity<LabTestCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__LabTestC__19093A0B026F19B1");

            entity.ToTable("LabTestCategory");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LabTestPrescription>(entity =>
        {
            entity.HasKey(e => e.LabTestPrescriptionId).HasName("PK__LabTestP__7E8F64EE042096EE");

            entity.ToTable("LabTestPrescription");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Appointment).WithMany(p => p.LabTestPrescriptions)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LabTestPr__Appoi__76969D2E");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.LabTestPrescriptions)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LabTestPr__Creat__787EE5A0");

            entity.HasOne(d => d.LabTest).WithMany(p => p.LabTestPrescriptions)
                .HasForeignKey(d => d.LabTestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LabTestPr__LabTe__778AC167");
        });

        modelBuilder.Entity<MainPrescription>(entity =>
        {
            entity.HasKey(e => e.MainPrescriptionId).HasName("PK__MainPres__68AF25A29C012C64");

            entity.ToTable("MainPrescription");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Appointment).WithMany(p => p.MainPrescriptions)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MainPresc__Appoi__5FB337D6");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MainPrescriptions)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MainPresc__Creat__60A75C0F");
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.MedicineId).HasName("PK__Medicine__4F212890CD45F0F8");

            entity.ToTable("Medicine");

            entity.Property(e => e.Cost)
                .HasMaxLength(50)
                .HasColumnName("cost");
            entity.Property(e => e.ExpiryDate).HasColumnType("date");
            entity.Property(e => e.ManufacturingDate).HasColumnType("date");
            entity.Property(e => e.MedicineName).HasMaxLength(100);

            entity.HasOne(d => d.MedicineCategory).WithMany(p => p.Medicines)
                .HasForeignKey(d => d.MedicineCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Medicine__Medici__656C112C");
        });

        modelBuilder.Entity<MedicineCategory>(entity =>
        {
            entity.HasKey(e => e.MedicineCategoryId).HasName("PK__Medicine__28C9BE8A670B2DBA");

            entity.ToTable("MedicineCategory");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<MedicineInventory>(entity =>
        {
            entity.HasKey(e => e.MedicineStockId).HasName("PK__Medicine__FA9527180B143DFA");

            entity.ToTable("MedicineInventory");

            entity.HasOne(d => d.Medicine).WithMany(p => p.MedicineInventories)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicineI__Medic__68487DD7");
        });

        modelBuilder.Entity<MedicinePrescription>(entity =>
        {
            entity.HasKey(e => e.MedicinePrescriptionId).HasName("PK__Medicine__4057AFDE1AD4026F");

            entity.ToTable("MedicinePrescription");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Dosage).HasMaxLength(50);
            entity.Property(e => e.Duration).HasMaxLength(50);
            entity.Property(e => e.Frequency).HasMaxLength(50);
            entity.Property(e => e.Quantity).HasMaxLength(50);

            entity.HasOne(d => d.Appointment).WithMany(p => p.MedicinePrescriptions)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicineP__Appoi__70DDC3D8");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MedicinePrescriptions)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicineP__Creat__72C60C4A");

            entity.HasOne(d => d.MainPrescription).WithMany(p => p.MedicinePrescriptions)
                .HasForeignKey(d => d.MainPrescriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicineP__MainP__73BA3083");

            entity.HasOne(d => d.Medicine).WithMany(p => p.MedicinePrescriptions)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicineP__Medic__71D1E811");
        });

        modelBuilder.Entity<NewAppointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__NewAppoi__8ECDFCC28C3C431C");

            entity.ToTable("NewAppointment");

            entity.Property(e => e.AppointmentDate).HasColumnType("date");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");

            entity.HasOne(d => d.Department).WithMany(p => p.NewAppointments)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__NewAppoin__Depar__59063A47");

            entity.HasOne(d => d.DocAvl).WithMany(p => p.NewAppointments)
                .HasForeignKey(d => d.DocAvlId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NewAppoin__DocAv__5629CD9C");

            entity.HasOne(d => d.Doctor).WithMany(p => p.NewAppointments)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NewAppoin__Docto__5812160E");

            entity.HasOne(d => d.Patient).WithMany(p => p.NewAppointments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NewAppoin__Patie__571DF1D5");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patient__970EC36640716714");

            entity.ToTable("Patient");

            entity.Property(e => e.Address).HasMaxLength(40);
            entity.Property(e => e.BloodGroup).HasMaxLength(3);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(35);
            entity.Property(e => e.Gender).HasMaxLength(6);
            entity.Property(e => e.PatientName).HasMaxLength(25);
            entity.Property(e => e.PhoneNumber).HasMaxLength(10);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1AFB7E1DD9");

            entity.ToTable("Role");

            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.SpecializationId).HasName("PK__Speciali__5809D86FFD01E487");

            entity.ToTable("Specialization");

            entity.Property(e => e.SpecializationName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Department).WithMany(p => p.Specializations)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Specializ__Depar__3D5E1FD2");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__96D4AB17D903206E");

            entity.Property(e => e.Address)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(35)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Qualification)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.StaffName)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Department).WithMany(p => p.Staff)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Staff__Departmen__403A8C7D");
        });

        modelBuilder.Entity<UserRegistration>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserRegi__1788CC4C8EC66C1A");

            entity.ToTable("UserRegistration");

            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.UserRegistrations)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRegis__RoleI__47DBAE45");

            entity.HasOne(d => d.Staff).WithMany(p => p.UserRegistrations)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRegis__Staff__46E78A0C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
