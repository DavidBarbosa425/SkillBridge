import { User } from '../../user-management';

export interface LoginResult {
  user: User;
  expiresIn: number;
}
