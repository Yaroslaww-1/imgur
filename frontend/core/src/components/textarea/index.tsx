import React from "react";

import styles from "./styles.module.scss";

interface ITextarea {
  [prop: string]: unknown;
}

export const Textarea: React.FC<ITextarea> = props => {
  return (
    <textarea
      {...props}
      className={styles.textarea}
    ></textarea>
  );
};
