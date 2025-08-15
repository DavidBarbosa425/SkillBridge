import { User } from './user';

export interface TokenResult {
  user: User;
  expiresIn: number;
}
