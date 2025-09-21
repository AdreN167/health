import { Stack } from "@mui/material";

const DishRow = ({ content }) => {
  return (
    <>
      <td>
        <span>{content.name}</span>
      </td>
      <td>
        <span>{content.description}</span>
      </td>
      <td>
        {content.products &&
          content.products.map((x) => (
            <Stack key={x.id} direction="row" justifyContent="space-between">
              <span style={{ maxWidth: 130 }}>{x.name}</span>
              <span>{x.weight} г</span>
            </Stack>
          ))}
      </td>
    </>
  );
};

export default DishRow;
