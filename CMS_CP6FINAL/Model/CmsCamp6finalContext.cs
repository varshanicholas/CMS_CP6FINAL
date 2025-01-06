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
            entity.HasKey(e => e.DailyAppointmentId).HasName("PK_DailyApp_A240DF944E350FFC");

            entity.ToTable("DailyAppointmentAvailability");

            entity.HasOne(d => d.Appointment).WithMany(p => p.DailyAppointmentAvailabilities)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DailyAppoAppoi_5812160E");

            entity.HasOne(d => d.DocAvl).WithMany(p => p.DailyAppointmentAvailabilities)
                .HasForeignKey(d => d.DocAvlId)
                .HasConstraintName("FK_DailyAppoDocAv_59063A47");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK_Departme_B2079BEDA148E6E4");

            entity.ToTable("Department");

            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Specialization).WithMany(p => p.Departments)
                .HasForeignKey(d => d.SpecializationId)
                .HasConstraintName("FK_DepartmenSpeci_3A81B327");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK_Doctor_2DC00EBF08B153EF");

            entity.ToTable("Doctor");

            entity.Property(e => e.ConsultationFee).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Staff).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DoctorStaffId_403A8C7D");
        });

        modelBuilder.Entity<DoctorAvailability>(entity =>
        {
            entity.HasKey(e => e.DocAvlId).HasName("PK_Doctor_A_387178EF3F56C47F");

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
                .HasConstraintName("FK_Doctor_AvDocto_46E78A0C");
        });

        modelBuilder.Entity<DoctorReferral>(entity =>
        {
            entity.HasKey(e => e.ReferralId).HasName("PK_DoctorRe_A2C4A96626B00866");

            entity.Property(e => e.ReferralDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Appointment).WithMany(p => p.DoctorReferrals)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DoctorRefAppoi_74AE54BC");

            entity.HasOne(d => d.Patient).WithMany(p => p.DoctorReferrals)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DoctorRefPatie_75A278F5");

            entity.HasOne(d => d.ReferredDoctor).WithMany(p => p.DoctorReferrals)
                .HasForeignKey(d => d.ReferredDoctorId)
                .HasConstraintName("FK_DoctorRefRefer_72C60C4A");

            entity.HasOne(d => d.ReferringDoctor).WithMany(p => p.DoctorReferrals)
                .HasForeignKey(d => d.ReferringDoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DoctorRefRefer_71D1E811");

            entity.HasOne(d => d.Specialization).WithMany(p => p.DoctorReferrals)
                .HasForeignKey(d => d.SpecializationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DoctorRefSpeci_73BA3083");
        });

        modelBuilder.Entity<LabReport>(entity =>
        {
            entity.HasKey(e => e.LabReportId).HasName("PK_LabRepor_971DB70DB2AD3094");

            entity.ToTable("LabReport");

            entity.Property(e => e.LabTestValue)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Remarks).HasMaxLength(255);

            entity.HasOne(d => d.Appointment).WithMany(p => p.LabReports)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK_LabReportAppoi_797309D9");

            entity.HasOne(d => d.LabTest).WithMany(p => p.LabReports)
                .HasForeignKey(d => d.LabTestId)
                .HasConstraintName("FK_LabReportLabTe_787EE5A0");

            entity.HasOne(d => d.LabTestPrescription).WithMany(p => p.LabReports)
                .HasForeignKey(d => d.LabTestPrescriptionId)
                .HasConstraintName("FK_LabReportLabTe_7A672E12");
        });

        modelBuilder.Entity<LabTest>(entity =>
        {
            entity.HasKey(e => e.LabTestId).HasName("PK_LabTest_64D33925893B34D9");

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
                .HasConstraintName("FK_LabTestCategor_4BAC3F29");
        });

        modelBuilder.Entity<LabTestCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK_LabTestC_19093A0B52C3DD56");

            entity.ToTable("LabTestCategory");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LabTestPrescription>(entity =>
        {
            entity.HasKey(e => e.LabTestPrescriptionId).HasName("PK_LabTestP_7E8F64EEA31FE60C");

            entity.ToTable("LabTestPrescription");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Appointment).WithMany(p => p.LabTestPrescriptions)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LabTestPrAppoi_6C190EBB");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.LabTestPrescriptions)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LabTestPrCreat_6E01572D");

            entity.HasOne(d => d.LabTest).WithMany(p => p.LabTestPrescriptions)
                .HasForeignKey(d => d.LabTestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LabTestPrLabTe_6D0D32F4");
        });

        modelBuilder.Entity<MainPrescription>(entity =>
        {
            entity.HasKey(e => e.MainPrescriptionId).HasName("PK_MainPres_68AF25A217937E57");

            entity.ToTable("MainPrescription");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Appointment).WithMany(p => p.MainPrescriptions)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MainPrescAppoi_5BE2A6F2");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MainPrescriptions)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MainPrescCreat_5CD6CB2B");
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.MedicineId).HasName("PK_Medicine_4F212890287024CA");

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
                .HasConstraintName("FK_MedicineMedici_619B8048");
        });

        modelBuilder.Entity<MedicineCategory>(entity =>
        {
            entity.HasKey(e => e.MedicineCategoryId).HasName("PK_Medicine_28C9BE8A343391DF");

            entity.ToTable("MedicineCategory");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<MedicineInventory>(entity =>
        {
            entity.HasKey(e => e.MedicineStockId).HasName("PK_Medicine_FA952718524D0629");

            entity.ToTable("MedicineInventory");

            entity.HasOne(d => d.Medicine).WithMany(p => p.MedicineInventories)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicineIMedic_6477ECF3");
        });

        modelBuilder.Entity<MedicinePrescription>(entity =>
        {
            entity.HasKey(e => e.MedicinePrescriptionId).HasName("PK_Medicine_4057AFDE84BD7838");

            entity.ToTable("MedicinePrescription");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Dosage).HasMaxLength(50);
            entity.Property(e => e.Duration).HasMaxLength(50);
            entity.Property(e => e.Frequency).HasMaxLength(50);
            entity.Property(e => e.Quantity).HasMaxLength(50);

            entity.HasOne(d => d.Appointment).WithMany(p => p.MedicinePrescriptions)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicinePAppoi_6754599E");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MedicinePrescriptions)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicinePCreat_693CA210");

            entity.HasOne(d => d.Medicine).WithMany(p => p.MedicinePrescriptions)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicinePMedic_68487DD7");
        });

        modelBuilder.Entity<NewAppointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK_NewAppoi_8ECDFCC26060CF7F");

            entity.ToTable("NewAppointment");

            entity.Property(e => e.AppointmentDate).HasColumnType("date");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");

            entity.HasOne(d => d.Department).WithMany(p => p.NewAppointments)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_NewAppoinDepar_5535A963");

            entity.HasOne(d => d.DocAvl).WithMany(p => p.NewAppointments)
                .HasForeignKey(d => d.DocAvlId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NewAppoinDocAv_52593CB8");

            entity.HasOne(d => d.Doctor).WithMany(p => p.NewAppointments)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NewAppoinDocto_5441852A");

            entity.HasOne(d => d.Patient).WithMany(p => p.NewAppointments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NewAppoinPatie_534D60F1");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK_Patient_970EC366AAEC901D");

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
            entity.HasKey(e => e.RoleId).HasName("PK_Role_8AFACE1AA1642534");

            entity.ToTable("Role");

            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.SpecializationId).HasName("PK_Speciali_5809D86F4C122476");

            entity.ToTable("Specialization");

            entity.Property(e => e.SpecializationName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK_Staff_96D4AB1700FB63FC");

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
                .HasConstraintName("FK_StaffDepartmen_3D5E1FD2");
        });

        modelBuilder.Entity<UserRegistration>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_UserRegi_1788CC4C960A576C");

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
                .HasConstraintName("FK_UserRegisRoleI_440B1D61");

            entity.HasOne(d => d.Staff).WithMany(p => p.UserRegistrations)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRegisStaff_4316F928");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}