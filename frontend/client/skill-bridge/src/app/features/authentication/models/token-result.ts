import { User } from '../../user-management';

export interface TokenResult {
  user: User;
  expiresIn: number;
}
