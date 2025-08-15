import { User } from './user.model';

export interface LoginResult {
  user: User;
  expiresIn: number;
}
