import { SelectItem } from "primeng/api";

export class Helper {
    public static enumToArray(enumObj: any): { key: string, value: number }[] {
        return Object.keys(enumObj)
          .filter(k => isNaN(Number(k)))   
          .map(key => ({ key, value: enumObj[key] }));
      }
    public static enumToSelectItems<T extends Record<string, string | number>>(
        enumObj: T,
        format: (label: string, value: T[keyof T]) => string = (label) => label
      ): SelectItem[] {
        return Object.keys(enumObj)
          .filter(key => isNaN(Number(key))) // exclude numeric keys from reverse mapping (for numeric enums)
          .map(key => {
            const value = enumObj[key as keyof T];
            return {
              label: format(key, value),
              value: value
            };
          });
}
}