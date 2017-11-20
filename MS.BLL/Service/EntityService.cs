using MS.BLL.Repository.Entity;

namespace MS.BLL.Service
{
    public class EntityService
    {
        public EntityService()
        {
            roomLectureRepository = new RoomLectureRepository();
            roomRepository = new RoomRepository();
            lectureRepository = new LectureRepository();
            parentRepository = new ParentRepository();
            postponedLessonRepository = new PostponedLessonRepository();
            programRepository = new ProgramRepository();
            recievedPaymentRepository = new RecievedPaymentRepository();
            studentRepository = new StudentRepository();
            teacherLectureRepository = new TeacherLectureRepository();
            teacherPaymentRepository = new TeacherPaymentRepository();
            teacherRepository = new TeacherRepository();
            userRepository = new UserRepository();
        }

        private RoomLectureRepository roomLectureRepository;
        public RoomLectureRepository roomLectureService
        {
            get { return roomLectureRepository; }
            set { roomLectureRepository = value; }
        }

        private RoomRepository roomRepository;
        public RoomRepository roomService
        {
            get { return roomRepository; }
            set { roomRepository = value; }
        }

        private LectureRepository lectureRepository;
        public LectureRepository lectureService
        {
            get { return lectureRepository; }
            set { lectureRepository = value; }
        }

        private ParentRepository parentRepository;
        public ParentRepository parentService
        {
            get { return parentRepository; }
            set { parentRepository = value; }
        }

        private PostponedLessonRepository postponedLessonRepository;
        public PostponedLessonRepository postponedLessonService
        {
            get { return postponedLessonRepository; }
            set { postponedLessonRepository = value; }
        }

        private ProgramRepository programRepository;
        public ProgramRepository programService
        {
            get { return programRepository; }
            set { programRepository = value; }
        }

        private RecievedPaymentRepository recievedPaymentRepository;
        public RecievedPaymentRepository recievedPaymentService
        {
            get { return recievedPaymentRepository; }
            set { recievedPaymentRepository = value; }
        }

        private StudentRepository studentRepository;
        public StudentRepository studentService
        {
            get { return studentRepository; }
            set { studentRepository = value; }
        }

        private TeacherLectureRepository teacherLectureRepository;
        public TeacherLectureRepository teacherLectureService
        {
            get { return teacherLectureRepository; }
            set { teacherLectureRepository = value; }
        }

        private TeacherPaymentRepository teacherPaymentRepository;
        public TeacherPaymentRepository teacherPaymentService
        {
            get { return teacherPaymentRepository; }
            set { teacherPaymentRepository = value; }
        }

        private TeacherRepository teacherRepository;
        public TeacherRepository teacherService
        {
            get { return teacherRepository; }
            set { teacherRepository = value; }
        }

        private UserRepository userRepository;
        public UserRepository userService
        {
            get { return userRepository; }
            set { userRepository = value; }
        }

    }
}
