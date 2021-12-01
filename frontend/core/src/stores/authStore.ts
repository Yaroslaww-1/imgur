import { makeAutoObservable } from "mobx";

import { AuthService } from "@api/services/auth.service";

import { IUser } from "@models/user.model";

export default class AuthStore {
  user = {} as IUser;
  isAuth = false;

  constructor() {
    makeAutoObservable(this);
  }

  setAuth(bool: boolean) {
    this.isAuth = bool;
  }

  setUser(user: IUser) {
    this.user = user;
  }

  async login(username: string, password: string) {
    try {
      const responseTokens = await AuthService.login(username, password);
      localStorage.setItem("accessToken", responseTokens.access_token);
      localStorage.setItem("refreshToken", responseTokens.refresh_token);
      this.setAuth(true);
      const responseUser = await AuthService.getAuthenticatedUser(
        username,
        password,
      );
      this.setUser(responseUser);
    } catch (e) {
      console.log(e);
    }
  }

  async registration(email: string, password: string) {
    try {
      const responseTokens = await AuthService.registration(email, password);
      localStorage.setItem("accessToken", responseTokens.access_token);
      localStorage.setItem("refreshToken", responseTokens.refresh_token);
      this.setAuth(true);
      const responseUser = await AuthService.getAuthenticatedUser(
        email,
        password,
      );
      this.setUser(responseUser);
    } catch (e) {
      console.log(e);
    }
  }

  async logout() {
    try {
      localStorage.removeItem("accessToken");
      localStorage.removeItem("refreshToken");
      this.setAuth(false);
      this.setUser({} as IUser);
    } catch (e) {
      console.log(e);
    }
  }

  async checkAuth() {
    try {
      const responseTokens = await AuthService.refresh(
        localStorage.getItem("refreshToken") || "",
      );
      localStorage.setItem("accessToken", responseTokens.access_token);
      localStorage.setItem("refreshToken", responseTokens.refresh_token);
      this.setAuth(true);
      const responseUser = await AuthService.getAuthenticatedUser();
      this.setUser(responseUser);
    } catch (e) {
      console.log(e);
    }
  }
}
