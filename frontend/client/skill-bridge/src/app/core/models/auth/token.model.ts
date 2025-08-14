import { User } from './user.model';

export interface TokenResult {
  token: string;
  refreshToken: string;
  user: User;
  expiresIn: number;
}
