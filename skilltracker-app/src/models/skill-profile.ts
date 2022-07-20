import { AssociateDetails } from "./associate-details";
import { Skill } from "./skill";

export class SkillProfile {
    public id!: number;
    public associateInfo!: AssociateDetails;
    public skillInfo!: Array<Skill>;
    public addedDate!: Date;
    public lastUpdatedDate!: Date;
}