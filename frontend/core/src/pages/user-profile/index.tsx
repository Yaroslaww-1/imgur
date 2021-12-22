import React, { useEffect, useState } from "react";

import { AuthService } from "@api/services/auth.service";
import { IUser } from "@models/user.model";

import { Page } from "@components/page";

import styles from "./styles.module.scss";

export const UserProfile: React.FC = () => {
  const [user, setUser] = useState<IUser>();

  useEffect(() => {
    AuthService.getAuthenticatedUser().then(user => {
      setUser(user);
    });
  }, []);

  return (
    <Page>
      <div className={styles.content}>
        <div className={styles.imageWrapper}>
          <i className="fa fa-user-circle-o fa-6"></i>
        </div>
        <div className={styles.info}>
          <div className={styles.label}>Name: {user?.name}</div>
          <div className={styles.label}>Email: {user?.email}</div>
          <div className={styles.label}>Role: {user?.roles[0].name}</div>
        </div>
      </div>
    </Page>
  );
};
