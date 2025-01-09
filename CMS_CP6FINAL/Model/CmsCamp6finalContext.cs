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

    public virtual DbSet<TimeSlot> TimeSlots { get; set; }

    public virtual DbSet<UserRegistration> UserRegistrations { get; set; }

    public virtual DbSet<Weekday> Weekdays { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source =SHAJITH-NICHOLA\\SQLEXPRESS; Initial Catalog = CMS_CAMP6FINAL; Integrated Security = True; Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DailyAppointmentAvailability>(entity =>
        {
            entity.HasKey(e => e.DailyAppointmentId).HasName("PK__DailyApp__A240DF9410A91E4F");

            entity.ToTable("DailyAppointmentAvailability");

            entity.HasOne(d => d.DocAvl).WithMany(p => p.DailyAppointmentAvailabilities)
                .HasForeignKey(d => d.DocAvlId)
                .HasConstraintName("FK__DailyAppo__DocAv__29221CFB");

            entity.HasOne(d => d.Patient).WithMany(p => p.DailyAppointmentAvailabilities)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DailyAppo__Patie__2A164134");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BED48A5BDFA");

            entity.ToTable("Department");

            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Specialization).WithMany(p => p.Departments)
                .HasForeignKey(d => d.SpecializationId)
                .HasConstraintName("FK__Departmen__Speci__2B0A656D");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__Doctor__2DC00EBF58441DDA");

            entity.ToTable("Doctor");

            entity.Property(e => e.ConsultationFee).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Staff).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Doctor__StaffId__2BFE89A6");
        });

        modelBuilder.Entity<DoctorAvailability>(entity =>
        {
            entity.HasKey(e => e.DocAvlId).HasName("PK__Doctor_A__387178EFE630F48B");

            entity.ToTable("Doctor_Availability");

            entity.HasOne(d => d.Doc).WithMany(p => p.DoctorAvailabilities)
                .HasForeignKey(d => d.DocId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Doctor_Av__DocId__2CF2ADDF");

            entity.HasOne(d => d.TimeSlot).WithMany(p => p.DoctorAvailabilities)
                .HasForeignKey(d => d.TimeSlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Doctor_Av__TimeS__2DE6D218");

            entity.HasOne(d => d.Week).WithMany(p => p.DoctorAvailabilities)
                .HasForeignKey(d => d.WeekId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Doctor_Av__WeekI__2EDAF651");
        });

        modelBuilder.Entity<DoctorReferral>(entity =>
        {
            entity.HasKey(e => e.ReferralId).HasName("PK__DoctorRe__A2C4A966960061DC");

            entity.Property(e => e.ReferralDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Appointment).WithMany(p => p.DoctorReferrals)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DoctorRef__Appoi__2FCF1A8A");

            entity.HasOne(d => d.Patient).WithMany(p => p.DoctorReferrals)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DoctorRef__Patie__30C33EC3");

            entity.HasOne(d => d.ReferredDoctor).WithMany(p => p.DoctorReferrals)
                .HasForeignKey(d => d.ReferredDoctorId)
                .HasConstraintName("FK__DoctorRef__Refer__32AB8735");

            entity.HasOne(d => d.ReferringDoctor).WithMany(p => p.DoctorReferrals)
                .HasForeignKey(d => d.ReferringDoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DoctorRef__Refer__31B762FC");

            entity.HasOne(d => d.Specialization).WithMany(p => p.DoctorReferrals)
                .HasForeignKey(d => d.SpecializationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DoctorRef__Speci__339FAB6E");
        });

        modelBuilder.Entity<LabReport>(entity =>
        {
            entity.HasKey(e => e.LabReportId).HasName("PK__LabRepor__971DB70DB78256CC");

            entity.ToTable("LabReport");

            entity.Property(e => e.LabTestValue)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Remarks).HasMaxLength(255);

            entity.HasOne(d => d.Appointment).WithMany(p => p.LabReports)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK__LabReport__Appoi__01142BA1");

            entity.HasOne(d => d.LabTest).WithMany(p => p.LabReports)
                .HasForeignKey(d => d.LabTestId)
                .HasConstraintName("FK__LabReport__LabTe__00200768");

            entity.HasOne(d => d.LabTestPrescription).WithMany(p => p.LabReports)
                .HasForeignKey(d => d.LabTestPrescriptionId)
                .HasConstraintName("FK__LabReport__LabTe__02084FDA");
        });

        modelBuilder.Entity<LabTest>(entity =>
        {
            entity.HasKey(e => e.LabTestId).HasName("PK__LabTest__64D339251F35E92F");

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
                .HasConstraintName("FK__LabTest__Categor__37703C52");
        });

        modelBuilder.Entity<LabTestCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__LabTestC__19093A0B89EEAFAA");

            entity.ToTable("LabTestCategory");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LabTestPrescription>(entity =>
        {
            entity.HasKey(e => e.LabTestPrescriptionId).HasName("PK__LabTestP__7E8F64EE5199411A");

            entity.ToTable("LabTestPrescription");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Appointment).WithMany(p => p.LabTestPrescriptions)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LabTestPr__Appoi__3864608B");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.LabTestPrescriptions)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LabTestPr__Creat__395884C4");

            entity.HasOne(d => d.LabTest).WithMany(p => p.LabTestPrescriptions)
                .HasForeignKey(d => d.LabTestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LabTestPr__LabTe__3A4CA8FD");
        });

        modelBuilder.Entity<MainPrescription>(entity =>
        {
            entity.HasKey(e => e.MainPrescriptionId).HasName("PK__MainPres__68AF25A2175B004F");

            entity.ToTable("MainPrescription");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Appointment).WithMany(p => p.MainPrescriptions)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MainPresc__Appoi__3B40CD36");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MainPrescriptions)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MainPresc__Creat__3C34F16F");
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.MedicineId).HasName("PK__Medicine__4F21289056601AA3");

            entity.ToTable("Medicine");

            entity.Property(e => e.ExpiryDate).HasColumnType("date");
            entity.Property(e => e.ManufacturingDate).HasColumnType("date");
            entity.Property(e => e.MedicineName).HasMaxLength(100);
            entity.Property(e => e.Unit).HasMaxLength(50);

            entity.HasOne(d => d.MedicineCategory).WithMany(p => p.Medicines)
                .HasForeignKey(d => d.MedicineCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Medicine__Medici__3D2915A8");
        });

        modelBuilder.Entity<MedicineCategory>(entity =>
        {
            entity.HasKey(e => e.MedicineCategoryId).HasName("PK__Medicine__28C9BE8A3BCCCCF6");

            entity.ToTable("MedicineCategory");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<MedicineInventory>(entity =>
        {
            entity.HasKey(e => e.MedicineStockId).HasName("PK__Medicine__FA952718B3A797A4");

            entity.ToTable("MedicineInventory");

            entity.HasOne(d => d.Medicine).WithMany(p => p.MedicineInventories)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicineI__Medic__3E1D39E1");
        });

        modelBuilder.Entity<MedicinePrescription>(entity =>
        {
            entity.HasKey(e => e.MedicinePrescriptionId).HasName("PK__Medicine__4057AFDE3770A1FD");

            entity.ToTable("MedicinePrescription");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Dosage).HasMaxLength(50);
            entity.Property(e => e.Duration).HasMaxLength(50);
            entity.Property(e => e.Frequency).HasMaxLength(50);
            entity.Property(e => e.Quantity).HasMaxLength(50);

            entity.HasOne(d => d.Appointment).WithMany(p => p.MedicinePrescriptions)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicineP__Appoi__3F115E1A");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MedicinePrescriptions)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicineP__Creat__40058253");

            entity.HasOne(d => d.Medicine).WithMany(p => p.MedicinePrescriptions)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicineP__Medic__40F9A68C");
        });

        modelBuilder.Entity<NewAppointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__NewAppoi__8ECDFCC29A555562");

            entity.ToTable("NewAppointment");

            entity.Property(e => e.AppointmentDate).HasColumnType("date");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");

            entity.HasOne(d => d.DailyAppointment).WithMany(p => p.NewAppointments)
                .HasForeignKey(d => d.DailyAppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NewAppoin__Daily__41EDCAC5");

            entity.HasOne(d => d.DocAvl).WithMany(p => p.NewAppointments)
                .HasForeignKey(d => d.DocAvlId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NewAppoin__DocAv__42E1EEFE");

            entity.HasOne(d => d.Doc).WithMany(p => p.NewAppointments)
                .HasForeignKey(d => d.DocId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NewAppoin__DocId__43D61337");

            entity.HasOne(d => d.Patient).WithMany(p => p.NewAppointments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NewAppoin__Patie__44CA3770");

            entity.HasOne(d => d.Specialization).WithMany(p => p.NewAppointments)
                .HasForeignKey(d => d.SpecializationId)
                .HasConstraintName("FK__NewAppoin__Speci__45BE5BA9");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patient__970EC366343A16AD");

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
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1A315F3EA6");

            entity.ToTable("Role");

            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.SpecializationId).HasName("PK__Speciali__5809D86F30DA4A61");

            entity.ToTable("Specialization");

            entity.Property(e => e.SpecializationName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__96D4AB17B8C2E4A4");

            entity.Property(e => e.Address)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(25)
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
                .HasConstraintName("FK__Staff__Departmen__3D5E1FD2");
        });

        modelBuilder.Entity<TimeSlot>(entity =>
        {
            entity.HasKey(e => e.TimeSlotId).HasName("PK__TimeSlot__41CC1F32E98256CB");

            entity.Property(e => e.TimeSlot1)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("TimeSlot");

            entity.HasOne(d => d.Wk).WithMany(p => p.TimeSlots)
                .HasForeignKey(d => d.WkId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TimeSlots__WkId__47A6A41B");
        });

        modelBuilder.Entity<UserRegistration>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserRegi__1788CC4CFE015A16");

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
                .HasConstraintName("FK__UserRegis__RoleI__440B1D61");

            entity.HasOne(d => d.Staff).WithMany(p => p.UserRegistrations)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRegis__Staff__4316F928");
        });

        modelBuilder.Entity<Weekday>(entity =>
        {
            entity.HasKey(e => e.WkId).HasName("PK__Weekdays__8065C7DDC32FE907");

            entity.Property(e => e.Day)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

