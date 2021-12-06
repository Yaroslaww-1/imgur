import React from "react";

import styles from "./styles.module.scss";

export const Page: React.FC = ({ children }) => {
  return <div className={styles.page}>{children}</div>;
};
