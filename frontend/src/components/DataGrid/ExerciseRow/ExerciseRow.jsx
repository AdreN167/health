const ExerciseRow = ({ content }) => {
  return (
    <>
      <td>
        <span>{content.name}</span>
      </td>
      <td>
        <span>{content.description}</span>
      </td>
      <td>
        <span>{content.caloriesBurned}</span>
      </td>
      <td>
        <span>{content.trainer ? content.trainer.name : "-"}</span>
      </td>
    </>
  );
};

export default ExerciseRow;
