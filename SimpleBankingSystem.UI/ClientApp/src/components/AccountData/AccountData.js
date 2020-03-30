import React from "react";
import styles from "./AccountData.module.css";
import DataElement from "../DataElement/DataElement";

const AccountData = props => {
  const serverUnavailableText = "Server unavailable!";
  const waitingForDataText = "Waiting for data...";
  const properNoDataText = !props.isServerAvailable
    ? serverUnavailableText
    : waitingForDataText;

  return (
    <div className={styles.AccountData}>
      <DataElement
        labelText="Account status:"
        value={props.status}
        noDataText={properNoDataText}
      />
      <DataElement
        labelText="Account balance:"
        value={props.balance}
        noDataText={properNoDataText}
      />
    </div>
  );
};

export default AccountData;
