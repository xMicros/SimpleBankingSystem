import React from "react";
import styles from "./DataElement.module.css";

const DataElement = props => {
  return (
    <p>
      <span className={styles.Label}>{props.labelText}</span>{" "}
      {!props.value ? props.noDataText : props.value}
    </p>
  );
};

export default DataElement;
