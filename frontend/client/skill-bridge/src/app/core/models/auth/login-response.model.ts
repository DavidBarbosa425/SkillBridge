import { User } from './user.model';

export interface LoginResultDto {
  token: string;
  refreshToken: string;
  user: User;
  expiresIn: number;
}
