import { User } from './user.model';

export interface LoginResult {
  token: string;
  refreshToken: string;
  user: User;
  expiresIn: number;
}
