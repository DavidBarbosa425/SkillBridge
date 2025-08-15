import { User } from './user';

export interface LoginResult {
  user: User;
  expiresIn: number;
}
