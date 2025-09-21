const ProductRow = ({ content }) => {
  return (
    <>
      <td>
        <span>{content.name}</span>
      </td>
      <td>
        <span>{content.calories}</span>
      </td>
      <td>
        <span>{content.proteins}</span>
      </td>
      <td>
        <span>{content.fats}</span>
      </td>
      <td>
        <span>{content.carbohydrates}</span>
      </td>
    </>
  );
};

export default ProductRow;
