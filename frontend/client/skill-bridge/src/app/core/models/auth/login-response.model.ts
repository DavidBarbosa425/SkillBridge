import { UserModel } from "./user.model";

export interface LoginResponse {
  token: string;
  refreshToken: string;
  user: UserModel;
  expiresIn: number;
}