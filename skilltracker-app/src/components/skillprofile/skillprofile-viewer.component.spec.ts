import { SkillProfile } from "src/models/skill-profile";
import { SkillProfileViewer } from "./skillprofile-viewer.component";

describe("Skill profile viewer", () => {
    it("should load the component successfully", () => {
        const component = new SkillProfileViewer();
        component.skillProfileList = [new SkillProfile(), new SkillProfile()];
        expect(component).not.toBeNull;
        expect(component.displayedColumns).toEqual(["skillName", "expertiseLevel"]);
        expect(component.skillProfileList.length).toBe(2);
    });
});