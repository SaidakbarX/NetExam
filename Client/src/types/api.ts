export interface User {
  id: number;
  email: string;
  role: string;
  createdAt: string;
}

export interface AuthResponse {
  user: User;
  accessToken: string;
  refreshToken: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  email: string;
  password: string;
  confirmPassword: string;
}

export interface RefreshTokenRequest {
  refreshToken: string;
}

export interface ProgrammingLanguage {
  id: number;
  name: string;
  version: string;
  fileExtension: string;
}

export interface CodeQuestion {
  id: number;
  title: string;
  description: string;
  difficulty: 'Easy' | 'Medium' | 'Hard';
  maxScore: number;
  timeLimit: number;
  programmingLanguageId: number;
  programmingLanguage?: ProgrammingLanguage;
  createdAt: string;
}

export interface TestCase {
  id: number;
  questionId: number;
  input: string;
  expectedOutput: string;
  isHidden: boolean;
}

export interface StarterTemplate {
  id: number;
  questionId: number;
  programmingLanguageId: number;
  code: string;
  programmingLanguage?: ProgrammingLanguage;
}

export interface Exam {
  id: number;
  title: string;
  description: string;
  duration: number;
  startTime: string;
  endTime: string;
  isActive: boolean;
  createdAt: string;
}

export interface ExamQuestion {
  id: number;
  examId: number;
  questionId: number;
  order: number;
  question?: CodeQuestion;
}

export interface CodeSubmission {
  id: number;
  userId: number;
  questionId: number;
  code: string;
  programmingLanguageId: number;
  score: number;
  executionTime: number;
  submittedAt: string;
  status: 'Pending' | 'Accepted' | 'Wrong Answer' | 'Time Limit Exceeded' | 'Runtime Error';
}

export interface ExamResult {
  id: number;
  userId: number;
  examId: number;
  totalScore: number;
  maxScore: number;
  completedAt: string;
  exam?: Exam;
  user?: User;
}