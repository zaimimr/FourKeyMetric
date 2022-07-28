import { Score } from "components/score";
import { COLORS } from "const";
import { IChangeFailureRate } from "types";
type ChangeFailureRateProps = { data: IChangeFailureRate };
export function ChangeFailureRateScore({ data }: ChangeFailureRateProps) {
  const [category, color] = cfr_category(data?.changeFailureRate);
  return (
    <Score category={category} color={color} title={"Change Failure Rate"} />
  );
}

export function cfr_category(rate: number) {
  if (rate <= 0.15) {
    return ["0-15%", COLORS.GREEN];
  } else if (rate <= 0.45) {
    return ["16-45%", COLORS.YELLOW];
  } else {
    return [">45%", COLORS.RED];
  }
}
