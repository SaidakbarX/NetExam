const API_BASE_URL = 'https://localhost:7001/api'; // Update with your API URL

// Mock user data for local development
const MOCK_USERS_KEY = 'mock_users';
const MOCK_TOKENS_KEY = 'mock_tokens';

// Helper functions for mock authentication
const getMockUsers = () => {
  const users = localStorage.getItem(MOCK_USERS_KEY);
  const parsedUsers = users ? JSON.parse(users) : [];
  
  // If no users exist, initialize with a default user
  if (parsedUsers.length === 0) {
    const defaultUser = {
      id: 1,
      email: 'test@example.com',
      password: 'password',
      role: 'Student',
      createdAt: new Date().toISOString()
    };
    parsedUsers.push(defaultUser);
    saveMockUsers(parsedUsers);
  }
  
  return parsedUsers;
};

const saveMockUsers = (users: any[]) => {
  localStorage.setItem(MOCK_USERS_KEY, JSON.stringify(users));
};

const generateMockToken = () => {
  return 'mock_token_' + Math.random().toString(36).substr(2, 9);
};

class ApiService {
  private getAuthHeaders(): HeadersInit {
    const token = localStorage.getItem('accessToken');
    return {
      'Content-Type': 'application/json',
      ...(token && { Authorization: `Bearer ${token}` })
    };
  }

  private async handleResponse<T>(response: Response): Promise<T> {
    if (!response.ok) {
      const error = await response.text();
      throw new Error(error || `HTTP error! status: ${response.status}`);
    }
    
    try {
      return await response.json();
    } catch {
      return {} as T;
    }
  }

  // Mock authentication methods for local development
  private async mockLogin(credentials: { email: string; password: string }) {
    const users = getMockUsers();
    const user = users.find((u: any) => u.email === credentials.email && u.password === credentials.password);
    
    if (!user) {
      throw new Error('Invalid email or password');
    }

    const token = generateMockToken();
    const refreshToken = generateMockToken();
    
    return {
      accessToken: token,
      refreshToken: refreshToken,
      user: {
        id: user.id,
        email: user.email,
        role: user.role || 'Student'
      }
    };
  }

  private async mockRegister(userData: { email: string; password: string; confirmPassword: string }) {
    const users = getMockUsers();
    
    // Check if user already exists
    if (users.find((u: any) => u.email === userData.email)) {
      throw new Error('User with this email already exists');
    }

    // Create new user
    const newUser = {
      id: users.length + 1,
      email: userData.email,
      password: userData.password,
      role: 'Student',
      createdAt: new Date().toISOString()
    };

    users.push(newUser);
    saveMockUsers(users);

    const token = generateMockToken();
    const refreshToken = generateMockToken();

    return {
      accessToken: token,
      refreshToken: refreshToken,
      user: {
        id: newUser.id,
        email: newUser.email,
        role: newUser.role
      }
    };
  }

  // Authentication methods with fallback to mock
  async login(credentials: { email: string; password: string }) {
    try {
      const response = await fetch(`${API_BASE_URL}/auth/login`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(credentials)
      });
      return this.handleResponse(response);
    } catch (error) {
      console.warn('Backend not available, using mock authentication');
      return this.mockLogin(credentials);
    }
  }

  async register(userData: { email: string; password: string; confirmPassword: string }) {
    try {
      const response = await fetch(`${API_BASE_URL}/auth/register`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(userData)
      });
      return this.handleResponse(response);
    } catch (error) {
      console.warn('Backend not available, using mock authentication');
      return this.mockRegister(userData);
    }
  }

  async refreshToken(refreshToken: string) {
    try {
      const response = await fetch(`${API_BASE_URL}/auth/refresh-token`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ refreshToken })
      });
      return this.handleResponse(response);
    } catch (error) {
      console.warn('Backend not available, using mock token refresh');
      const newToken = generateMockToken();
      return {
        accessToken: newToken,
        refreshToken: generateMockToken()
      };
    }
  }

  async logout() {
    try {
      const response = await fetch(`${API_BASE_URL}/auth/logout`, {
        method: 'POST',
        headers: this.getAuthHeaders()
      });
      return this.handleResponse(response);
    } catch (error) {
      console.warn('Backend not available, performing local logout');
      return { success: true };
    }
  }

  // Programming Languages
  async getProgrammingLanguages() {
    const response = await fetch(`${API_BASE_URL}/programming-languages`, {
      headers: this.getAuthHeaders()
    });
    return this.handleResponse(response);
  }

  async createProgrammingLanguage(data: any) {
    const response = await fetch(`${API_BASE_URL}/programming-languages`, {
      method: 'POST',
      headers: this.getAuthHeaders(),
      body: JSON.stringify(data)
    });
    return this.handleResponse(response);
  }

  // Code Questions
  async getCodeQuestions() {
    const response = await fetch(`${API_BASE_URL}/code-questions`, {
      headers: this.getAuthHeaders()
    });
    return this.handleResponse(response);
  }

  async getCodeQuestion(id: number) {
    const response = await fetch(`${API_BASE_URL}/code-questions/${id}`, {
      headers: this.getAuthHeaders()
    });
    return this.handleResponse(response);
  }

  async createCodeQuestion(data: any) {
    const response = await fetch(`${API_BASE_URL}/code-questions`, {
      method: 'POST',
      headers: this.getAuthHeaders(),
      body: JSON.stringify(data)
    });
    return this.handleResponse(response);
  }

  async updateCodeQuestion(id: number, data: any) {
    const response = await fetch(`${API_BASE_URL}/code-questions/${id}`, {
      method: 'PUT',
      headers: this.getAuthHeaders(),
      body: JSON.stringify(data)
    });
    return this.handleResponse(response);
  }

  async deleteCodeQuestion(id: number) {
    const response = await fetch(`${API_BASE_URL}/code-questions/${id}`, {
      method: 'DELETE',
      headers: this.getAuthHeaders()
    });
    return this.handleResponse(response);
  }

  // Exams
  async getExams() {
    const response = await fetch(`${API_BASE_URL}/exams`, {
      headers: this.getAuthHeaders()
    });
    return this.handleResponse(response);
  }

  async getExam(id: number) {
    const response = await fetch(`${API_BASE_URL}/exams/${id}`, {
      headers: this.getAuthHeaders()
    });
    return this.handleResponse(response);
  }

  async createExam(data: any) {
    const response = await fetch(`${API_BASE_URL}/exams`, {
      method: 'POST',
      headers: this.getAuthHeaders(),
      body: JSON.stringify(data)
    });
    return this.handleResponse(response);
  }

  // Exam Questions
  async getExamQuestions(examId: number) {
    const response = await fetch(`${API_BASE_URL}/exam-questions/by-exam/${examId}`, {
      headers: this.getAuthHeaders()
    });
    return this.handleResponse(response);
  }

  async addExamQuestion(data: any) {
    const response = await fetch(`${API_BASE_URL}/exam-questions`, {
      method: 'POST',
      headers: this.getAuthHeaders(),
      body: JSON.stringify(data)
    });
    return this.handleResponse(response);
  }

  // Code Submissions
  async getCodeSubmissions() {
    const response = await fetch(`${API_BASE_URL}/code-submissions`, {
      headers: this.getAuthHeaders()
    });
    return this.handleResponse(response);
  }

  async submitCode(data: any) {
    const response = await fetch(`${API_BASE_URL}/code-submissions`, {
      method: 'POST',
      headers: this.getAuthHeaders(),
      body: JSON.stringify(data)
    });
    return this.handleResponse(response);
  }

  async getUserSubmissions(userId: number) {
    const response = await fetch(`${API_BASE_URL}/code-submissions/by-user/${userId}`, {
      headers: this.getAuthHeaders()
    });
    return this.handleResponse(response);
  }

  // Exam Results
  async getExamResults(examId: number) {
    const response = await fetch(`${API_BASE_URL}/exam-results/by-exam/${examId}`, {
      headers: this.getAuthHeaders()
    });
    return this.handleResponse(response);
  }

  async getUserExamResults(userId: number) {
    const response = await fetch(`${API_BASE_URL}/exam-results/by-user/${userId}`, {
      headers: this.getAuthHeaders()
    });
    return this.handleResponse(response);
  }

  async submitExamResult(data: any) {
    const response = await fetch(`${API_BASE_URL}/exam-results`, {
      method: 'POST',
      headers: this.getAuthHeaders(),
      body: JSON.stringify(data)
    });
    return this.handleResponse(response);
  }

  // Test Cases
  async getTestCases(questionId: number) {
    const response = await fetch(`${API_BASE_URL}/test-cases/by-question/${questionId}`, {
      headers: this.getAuthHeaders()
    });
    return this.handleResponse(response);
  }

  async createTestCase(data: any) {
    const response = await fetch(`${API_BASE_URL}/test-cases`, {
      method: 'POST',
      headers: this.getAuthHeaders(),
      body: JSON.stringify(data)
    });
    return this.handleResponse(response);
  }

  // Starter Templates
  async getStarterTemplates(questionId: number) {
    const response = await fetch(`${API_BASE_URL}/starter-templates/by-question/${questionId}`, {
      headers: this.getAuthHeaders()
    });
    return this.handleResponse(response);
  }

  async createStarterTemplate(data: any) {
    const response = await fetch(`${API_BASE_URL}/starter-templates`, {
      method: 'POST',
      headers: this.getAuthHeaders(),
      body: JSON.stringify(data)
    });
    return this.handleResponse(response);
  }

  // Users
  async updateUserRole(userId: number, role: string) {
    const response = await fetch(`${API_BASE_URL}/users/${userId}/role?userRole=${role}`, {
      method: 'PUT',
      headers: this.getAuthHeaders()
    });
    return this.handleResponse(response);
  }

  async deleteUser(userId: number, userRole: string) {
    const response = await fetch(`${API_BASE_URL}/users/${userId}?userRole=${userRole}`, {
      method: 'DELETE',
      headers: this.getAuthHeaders()
    });
    return this.handleResponse(response);
  }
}

export const apiService = new ApiService();