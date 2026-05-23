// --- User (frontend-version) ---
export interface User {
  id: string;
  userName: string;
  email: string;
  displayName: string;
  avatarUrl?: string;
  points: number;
  bio?: string;
  role?: 'User' | 'Admin';
  createdAt?: string;
}

// --- Auth Responses ---
export interface AuthResponse {
  accessToken: string;
  refreshToken: string;
}

// --- Auth Requests ---
export interface RefreshTokenRequest {
  refreshToken: string;
}

// --- Register Request ---
export interface RegisterRequest {
  email: string;
  userName: string;
  password: string;
  signupCode: string;
}

// --- Login Request ---
export interface LoginRequest {
  email: string;
  password: string;
}

// --- JWT Payload ---
export interface TokenPayload {
  sub: string;
  userName: string;
  'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'?: string;
}